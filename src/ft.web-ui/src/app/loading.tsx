import LoadingSpinner from '@/components/LoadingSpinner'
import React from 'react'

export default function LoadingPage() {
  return (
    <div className="flex h-screen items-center justify-center bg-gray-500">
      <LoadingSpinner />
    </div>
  )
}
