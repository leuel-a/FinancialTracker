'use client'

import React, { useState, useEffect } from 'react'
import Image from 'next/image'

// Images for the home page
import goals from '../../../public/goals.svg'
import invest from '../../../public/invest.svg'
import progress from '../../../public/progress_data.svg'

export type HomePageImage = {
  image: string
  title: string
  description: string
}

type ImageSliderProps = {
  imageUrls: string[]
}

const homePageImages: HomePageImage[] = [
  {
    image: goals,
    title: 'Set your goals',
    description: 'Set goals and track your progress'
  },
  {
    image: invest,
    title: 'Invest your data',
    description: 'Invest your data and get insights'
  },
  {
    image: progress,
    title: 'Track your progress',
    description: 'Track your progress and improve'
  }
]

export default function ImageSlider() {
  const [imageIndex, setImageIndex] = useState<number>(0)

  useEffect(() => {
    const interval = setInterval(() => {
      setImageIndex(prevIndex => (prevIndex + 1) % homePageImages.length)
    }, 5000)

    return () => clearInterval(interval)
  }, [])

  return (
    <div className='flex flex-col justify-between h-[500px] items-center'>
      <div className="flex w-full flex-col items-center justify-center gap-8 ">
        <Image
          src={homePageImages[imageIndex].image}
          alt={homePageImages[imageIndex].title}
          width={400}
          height={400}
        />
        <div>
          <h2 className="text-3xl font-semibold text-white">{homePageImages[imageIndex].title}</h2>
          <p className="text-md font-light text-gray-200">
            {homePageImages[imageIndex].description}
          </p>
        </div>
      </div>
      <div className="flex gap-3">
        {homePageImages.map((_, index) => (
          <button
            key={index}
            onClick={() => setImageIndex(index)}
            className={`h-2 w-2 rounded-full ${index === imageIndex ? 'bg-white' : 'bg-gray-300'}`}
          />
        ))}
      </div>
    </div>
  )
}
