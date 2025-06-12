'use client'
import MainLayout from '@/components/Layouts/Layout';
import React from 'react';

export default function HomeLayout({ children }) {
  return (
    <>
      <MainLayout >
        {children}
      </MainLayout>
    </>
  )
}

