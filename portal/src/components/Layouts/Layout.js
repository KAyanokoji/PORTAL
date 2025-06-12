'use client'
import React from 'react'
import { SidebarProvider, useSidebar } from '../navigation/sidebar-provider'
import AppSidebar from '../navigation/app-sidebar'
import Content from '../navigation/content'
import Header from '../navigation/header'
import Footer from '../navigation/footer'
import SidebarTriggers from '../navigation/sidebar-triggers'

const LayoutInner = ({ children }) => {
  return (
    <div className="h-screen flex flex-col md:flex-row overflow-hidden bg-gray-100">
      <div className="h-full w-full md:w-[250px] overflow-y-auto overflow-x-hidden scrollbar-none">
        <AppSidebar />
      </div>

      <SidebarTriggers />

      <div className="flex flex-col flex-1 h-full">
        <div className="sticky top-0 z-10">
          <Header />
        </div>

        <main className="flex-1 overflow-y-auto p-1">
          <Content>{children}</Content>
        </main>

        <Footer />
      </div>
    </div>
  );
}


const MainLayout = ({ children }) => (
  <SidebarProvider>
    <LayoutInner>{children}</LayoutInner>
  </SidebarProvider>
);

export default MainLayout;