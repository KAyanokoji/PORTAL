'use client'
import { useAuth } from '@/components/hooks/useAuth';

export default function AuthWrapper({ children }) {
 
  useAuth() // Handles token validation and redirection
    return <>{ children }</>
 
}