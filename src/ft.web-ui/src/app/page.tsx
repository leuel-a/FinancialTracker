import React from 'react'
import axios from 'axios'
import Image from 'next/image'
import { redirect } from 'next/navigation'
import { Input } from '@/components/ui/input'
import { Button } from '@/components/ui/button'
import ErrorMessage from './components/ErrorMessage'
import ImageSlider from './components/ImageSlider'
import LoginForm from '../features/auth/components/LoginForm'

interface HomeProps {
  searchParams?: { [key: string]: string | string[] | undefined }
}

export default function Home({ searchParams }: HomeProps) {
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
      <div className="flex flex-1 flex-col items-center justify-center bg-blue-500">
        <ImageSlider />
      </div>
    </div>
  )
}
