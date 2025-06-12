'use client'
import React from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { useAuth } from "@/components/hooks/useAuth";

const DropdownMenu = () => {
    const router = useRouter();
    useAuth({
        onAuthenticated: () => router.push('/home'),
        redirectPath: '/login',
        requireAuth: false, // allow unauthenticated users to stay here
    })

    const handleLogout = () => {
        localStorage.clear();
    };

    return (
        <div className="absolute right mt-30 transform -translate-x-full w-48 bg-white border border-gray-200 rounded-md shadow-lg opacity-0 invisible group-hover:opacity-100 group-hover:visible transition-all duration-300 z-50">
            <ul className="py-1">
                <li>
                    <Link href="/profile" className="block px-4 py-2 text-gray-800 hover:bg-gray-100">
                        Profile
                    </Link>
                </li>
                <li>
                    <Link href="/settings" className="block px-4 py-2 text-gray-800 hover:bg-gray-100">
                        Settings
                    </Link>
                </li>
                <li>
                    <Link href="" onClick={handleLogout} className="block px-4 py-2 text-gray-800 hover:bg-gray-100">
                        Logout
                    </Link>
                </li>
            </ul>
        </div>
    );
};
export default DropdownMenu;