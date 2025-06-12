'use client'
import React, { useEffect } from 'react'
import { useSidebar } from './sidebar-provider';
import { useState } from 'react';
import {
  ChevronRight,
  ChevronDown,
  File,
  Folder,
  Trash,
  Star,
  Video,
  Book,
  Image,
} from 'lucide-react';
import { motion, AnimatePresence } from 'framer-motion';
import debounce from 'lodash.debounce';
import { useMemo } from 'react';
import menuItems from  '@/utils/menus.json'
import * as Icons from 'lucide-react';

const ITEMS = [
  {
    id: '1',
    label: 'Documents',
    icon: Folder,
    children: [
      { id: '1.1', label: 'Company', icon: Folder },
      { id: '1.2', label: 'Personal', icon: Folder },
      { id: '1.3', label: 'Group photo', icon: Image },
    ],
  },
  {
    id: '2',
    label: 'Bookmarked',
    icon: Star,
    children: [
      { id: '2.1', label: 'Learning materials', icon: Folder },
      { id: '2.2', label: 'News', icon: Folder },
      { id: '2.3', label: 'Forums', icon: Folder },
      { id: '2.4', label: 'Travel documents', icon: File },
    ],
  },
  { id: '3', label: 'History', icon: Book },
  { id: '4', label: 'Trash', icon: Trash },
  { id: '5', label: 'Video conference', icon: Video },

  // Duplicate the list
  {
    id: '6',
    label: 'Documents',
    icon: Folder,
    children: [
      { id: '6.1', label: 'Company', icon: Folder },
      { id: '6.2', label: 'Personal', icon: Folder },
      { id: '6.3', label: 'Group photo', icon: Image },
    ],
  },
  {
    id: '7',
    label: 'Bookmarked',
    icon: Star,
    children: [
      { id: '7.1', label: 'Learning materials', icon: Folder },
      { id: '7.2', label: 'News', icon: Folder },
      { id: '7.3', label: 'Forums', icon: Folder },
      { id: '7.4', label: 'Travel documents', icon: File },
    ],
  },
  { id: '8', label: 'History', icon: Book },
  { id: '9', label: 'Trash', icon: Trash },
  { id: '10', label: 'Video conference', icon: Video },
];

const iconMap = {
  Folder: Icons.Folder,
  File: Icons.File,
  Trash: Icons.Trash,
  Star: Icons.Star,
  Video: Icons.Video,
  Book: Icons.Book,
  Image: Icons.Image,
};
console.log("Menu Items",menuItems)
const transformedMenuItems = menuItems.map((item) => ({
  ...item,
  icon: iconMap[item.icon] || Icons.Folder, // fallback icon
  children: item.children?.map((child) => ({
    ...child,
    icon: iconMap[child.icon] || Icons.File,
  })),
}));
const SidebarItem = ({ label, Icon, isChild = false, onClick, hasChild, isExpanded }) => (

  
  <div
    className={`flex items-center justify-between px-4 py-2 rounded hover:bg-gray-700 cursor-pointer ${isChild ? 'pl-8 text-sm text-gray-300' : ''}`}
    onClick={onClick}
  >
    <div className="flex items-center gap-2">
      <Icon className="w-4 h-4" />
      <span className="truncate">{label}</span>
    </div>

    {!isChild && hasChild && (
      isExpanded ? (
        <ChevronDown className="w-4 h-4" />
      ) : (
        <ChevronRight className="w-4 h-4" />
      )
    )}
  </div>
);

const AppSidebar = () => {
  const { isOpen, closeSidebar ,toggleSidebar} = useSidebar();

 const [openSections, setOpenSections] = useState({});  // Track which section is expanded

 // Lock body scroll when sidebar is open on small screens
 useEffect(() => {
  if (isOpen && window.innerWidth < 768) {
    document.body.style.overflow = 'hidden';
  } else {
    document.body.style.overflow = '';
  }

  return () => {
    document.body.style.overflow = '';
  };
}, [isOpen]);
  
// Memoized debounced toggleSection function
const debouncedToggleSection = useMemo(
  () => debounce((sectionId) => {
    setOpenSections((prev) => ({
      ...Object.keys(prev).reduce((acc, key) => ({ ...acc, [key]: false }), {}),
      [sectionId]: !prev[sectionId],
    }));
  }, 300),
  []
);

const toggleSection = (sectionId) => {
  debouncedToggleSection(sectionId);
};

  return (
    // <div  className={`fixed top-0 left-0 z-50 h-full w-64 bg-gray-800 text-white shadow-lg
    //       transform transition-transform duration-300 ease-in-out
    //       ${isOpen ? 'translate-x-0' : '-translate-x-full'}
    //       md:relative md:translate-x-0`}>
    <div className={`
      fixed top-0 left-0 z-50 h-full w-64 bg-gray-800 text-white shadow-lg
      transform transition-transform duration-300 ease-in-out
      ${isOpen ? 'translate-x-0' : '-translate-x-full'}
      md:relative md:translate-x-0
      flex flex-col
    `}>
      {/* Sticky Header */}
      <div className=" flex items-center justify-between p-4 border-b border-gray-700 flex-shrink-0 sticky top-0 bg-gray-800 z-10">
        <h2 className="text-lg font-bold">My Files</h2>
        <button
          onClick={toggleSidebar}
          className="md:hidden text-gray-300 hover:text-white"
        >
          ✕
        </button>
      </div>

      {/* Scrollable Content */}
      {/* <div className="flex-1 overflow-y-auto overflow-x-hidden py-2 space-y-1 scrollbar-none scroll-smooth"> */}
      <div className="flex-1  overflow-y-auto overflow-x-hidden  space-y-1 scrollbar-none scroll-smooth max-h-[calc(100vh-130px)] md:max-h-[calc(100vh-130px)] ">
      {transformedMenuItems.map((item) => (
          <div key={item.id} className=''>
            {/* Main Item (Parent-level) */}
            <SidebarItem
              label={item.label}
              Icon={item.icon}
              isChild={false}  // This ensures the parent item doesn't get padding
              hasChild={item.children ? true : false}  // Pass `hasChild` based on whether there are children
              isExpanded={item.children ? openSections[item.id] : undefined}
              onClick={() => item.children && toggleSection(item.id)}
            />
            {item.children && (
                                    console.log("ITEM",item),

              <AnimatePresence>
                {openSections[item.id] && (
                  <motion.div
                    className="ml-4"
                    key={item.id}
                    initial={{ opacity: 0, y: -4 }}
                    animate={{ opacity: 1, y: 0 }}
                    exit={{ opacity: 0, y: -4 }}
                    transition={{ duration: 0.3 }}
                  >
                    {item.children.map((child) => (
                      <SidebarItem
                        key={child.id}
                        label={child.label}
                        Icon={child.icon}
                        isChild={true}
                      />
                    ))}
                  </motion.div>
                )}
              </AnimatePresence>
            )}

          </div>
        ))}
      </div>



      {/* Sticky Footer */}
      <div className="p-4 border-t border-gray-700 flex-shrink-0 sticky bottom-0 bg-gray-800 z-10 text-sm text-gray-400">
        <p>Storage: 3.5 GB / 15 GB</p>
        <button className="mt-2 text-blue-400 hover:underline">Manage Storage</button>
      </div>
    </div>
  );
};


export default AppSidebar