'use client'
import { useDispatch } from "react-redux"
import { useRouter } from "next/navigation"
import { useCallback, useEffect, useRef, useState } from "react"
import { jwtDecode } from "jwt-decode"
import { setUser, setRemoveUser } from "@/redux/auth/authSlice"


export const useAuth = (options = {}) => {
    const {
        tokenKey = 'authToken',
        redirectPath = '/login',
        redirectOnAuthPath = '/home', 
        requireAuth = true,
        onUnauthenticated,
        onAuthenticated,
    } = options


    const dispatch = useDispatch()
    const router = useRouter()

    const [isValidating, setIsValidating] = useState(false);

    const redirecting = useRef(false);

    const validateToken = useCallback(() => {
        setIsValidating(true); //
        const token = localStorage.getItem(tokenKey);

        // No token case
        if (!token) {
            handleUnauthenticated();
            return null;
        }

        try {
            const decode = jwtDecode(token)
            const currentTime = Math.floor(Date.now() / 1000);

            if (decode.exp && decode.exp < currentTime) {
                handleUnauthenticated();
                return null;
            }

            // Valid token case
            dispatch(setUser(decode))
            onAuthenticated?.(decode)
            return decode;
        } catch (error) {
            console.error('Token validation error:', error)
            handleUnauthenticated();
            return null;
        }
        finally {
            setIsValidating(false);
        }
    }, [dispatch, onAuthenticated])


    const handleUnauthenticated = useCallback(() => {
        localStorage.removeItem(tokenKey)
        dispatch(setRemoveUser())
        redirecting.current = false;
        onUnauthenticated?.()
        if (requireAuth && !redirecting.current) {
            redirecting.current = true
            router.push(redirectPath)
        }
    }, [dispatch, redirectPath, requireAuth, router, onUnauthenticated])

    useEffect(() => {
        const abortController = new AbortController();

        const validate = async () => {
            if (!abortController.signal.aborted) {
                validateToken();
            }
        };
        validate();
        return () => abortController.abort();
    }, [validateToken])

    return {
        validateToken, // Can be called manually if needed
        checkAuth: () => {
            const user = validateToken()
            if (!user && requireAuth) {
                router.push(redirectPath)
            }
            return user
        }
    }
}