import React from 'react'
import CardHeader from './card-header'
import CardContent from './card-content'
import CardFooter from './card-footer'
import CardImage from './card-image'

const Card = () => {
  return (
    <div className='bg-white max-w-sm md:max-w-md lg:max-w-lg rounded-2xl p-6 shadow-md'>
      <CardImage />
      <CardHeader />
      <CardContent />
      <CardFooter />
    </div>
  )
}

export default Card