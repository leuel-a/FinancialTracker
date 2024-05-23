import React from 'react'
import { redirect } from 'next/navigation'
import ImageSlider from './components/ImageSlider'
import { isAuthenticated } from '@/lib/auth.server'
import LoginForm from '../features/auth/components/LoginForm'

interface HomeProps {
  searchParams?: { [key: string]: string | string[] | undefined }
}

export default function Home({ searchParams }: HomeProps) {
  const isAuth = isAuthenticated()

  if (isAuth) {
    return redirect('/dashboard')
  }

  return (
    <div className="flex h-screen">
      <div className="flex flex-1 flex-col items-center justify-center">
        <div className="flex w-[540px] flex-col gap-5">
          <div>
            <h1 className="mb-2 text-2xl font-semibold">Log In into your Account</h1>
            <p className="text-md font-light text-gray-500">
              Welcome back. Please login with your credentials.
            </p>
          </div>
          <LoginForm />
          <p className="text-sm text-gray-500">
            If you do not have your login credentials yet, please contact your{' '}
            <span className="text-blue-500 underline">manager</span> to get one.
          </p>
        </div>
      </div>
      <div className="flex flex-1 flex-col items-center justify-center bg-zinc-800">
        <ImageSlider />
      </div>
    </div>
  )
}
