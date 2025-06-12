'use client'
import { useRouter } from 'next/navigation'

export default function Unauthorized() {
  const router = useRouter()
  
  return (
    <div className="flex flex-col items-center justify-center h-screen">
      <h1 className="text-2xl font-bold mb-4">Access Denied</h1>
      <p className="mb-6">You don't have permission to view this page</p>
      <button 
        onClick={() => router.push('/')}
        className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
      >
        Go to Home
      </button>
    </div>
  )
}