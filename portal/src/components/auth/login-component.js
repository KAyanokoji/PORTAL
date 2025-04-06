'use client'
import Button from '@/components/common/control/button';
import EmailInput from '@/components/common/control/email-input'
import PasswordInput from '@/components/common/control/password-input';
import ImageLogo from '@/components/common/pickers/image'
import { loginUser } from '@/redux/auth/authSlice';
import React, { useState } from 'react'
import { useDispatch } from 'react-redux';

const LoginComponent = () => {
    const dispatch = useDispatch()
    const[email, setEmail] = useState('');
    const[password, setPassword] = useState('');
    console.log("PAGE EMAIL", email)
    const handleSubmit=()=>{
        let formData = {
            username:email,
            password:password
        }
        dispatch(loginUser(formData))
    }

    return (
        <>
            <div className='flex items-center justify-center h-screen bg-gray-100'>
                <div className='flex flex-col w-[90vw] min-w-[280px] max-w-md sm:max-w-lg md:max-w-xl lg:max-w-2xl bg-white border border-gray-200 p-4 sm:p-6 rounded-lg shadow-lg mx-auto my-8 '>
                    <ImageLogo
                        src="/images/tu-logo.png"
                        alt="Logo"
                        width="w-32 sm:w-40"
                        height="h-12 sm:h-16"
                        className="cursor-pointer mx-auto"
                    />
                    <h4 className='text-center my-2 sm:my-3 text-lg sm:text-xl font-bold'>
                        Welcome to login
                    </h4>

                    <div className='space-y-4 sm:space-y-5 w-full'>
                        <EmailInput
                            label="Email"
                            value={email}
                            onChange={(email) => setEmail(email)}
                            required
                        />

                        <PasswordInput
                            label="Password"
                            showStrengthIndicator
                            value={password}
                            onChange={(password ) => setPassword( password)}
                            required
                        />

                        <Button
                            variant="primary"
                            style="outlined"
                            size="medium"
                            shape="rounded"
                            label='Login'
                            width='full'
                            onClick={handleSubmit}
                        />
                    </div>

                    <a href='/#' className='mt-3 sm:mt-4 text-sm sm:text-base text-blue-600 hover:text-blue-800 self-end'>
                        Forgot password?
                    </a>
                </div>

            </div>
        </>
    )
}

export default LoginComponent