'use client'

import Cookies from 'js-cookie'
import LoadingSpinner from '@/components/LoadingSpinner'
import PageTitle from '../dashboard/components/PageTitle'
import { useEffect, useState } from 'react'
import { useRouter } from 'next/navigation'

export default function LogoutPage() {
  const [waitTime, setWaitTime] = useState(5)
  const router = useRouter()

  useEffect(() => {
    const interval = setInterval(() => {
      setWaitTime(prev => prev - 1)
    }, 1000) // Decrement the counter every second

    // Clear the interval when the component unmounts
    return () => clearInterval(interval)
  }, [])

  useEffect(() => {
    if (waitTime <= 0) {
      Cookies.remove('accessToken')
      Cookies.remove('refreshToken')
      router.push('/')
    }
  }, [waitTime, router])

  return (
    <div className="flex h-screen flex-col items-center justify-center gap-10">
      <PageTitle title={`We are currently logging you out. Please wait ${waitTime} seconds.`} />
      <LoadingSpinner className="h-20 w-20" />
    </div>
  )
}
