'use client'
import React from 'react'

const Footer = () => {
  return (
    // <footer className="bg-white border-t text-center text-sm text-gray-600 py-3">
    <footer className="p-4 border-t border-gray-700 flex-shrink-0 sticky bottom-0 bg-white  z-10 text-sm text-gray-400">
      © {new Date().getFullYear()} MyApp. All rights reserved.
    </footer>
  )
}

export default Footer