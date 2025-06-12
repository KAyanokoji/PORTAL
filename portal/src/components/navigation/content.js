import React from 'react'

const Content = ({children}) => {
  return (
   
    <main className="flex-1 bg-white overflow-y-auto overflow-x-hidden py-2 space-y-1 ">
    {children || 'Content'}
  </main>
  )
}

export default Content