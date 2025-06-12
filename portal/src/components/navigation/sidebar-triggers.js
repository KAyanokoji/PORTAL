import React from 'react'
import { useSidebar } from './sidebar-provider'
import { Menu } from 'lucide-react'

const SidebarTriggers = () => {
  const { isOpen, toggleSidebar } = useSidebar();
  return (
    <div className="md:hidden fixed top-4 left-4 z-[100]">
       {!isOpen && (
        <button
          onClick={toggleSidebar}
          className="p-2 rounded bg-white text-gray-800 shadow-sm"
        >
          <Menu className="h-6 w-6" />
        </button>
      )}
    </div>
  )
}

export default SidebarTriggers