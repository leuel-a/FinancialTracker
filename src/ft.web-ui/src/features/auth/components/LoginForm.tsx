'use client'

import { z } from 'zod'
import { useRouter } from 'next/navigation'
import { useForm } from 'react-hook-form'
import { Eye, EyeOff } from 'lucide-react'
import { loginUser } from '../authActions'
import { useState, useEffect } from 'react'
import { Input } from '@/components/ui/input'
import { Button } from '@/components/ui/button'
import { zodResolver } from '@hookform/resolvers/zod'
import { useDispatch, useSelector } from '@/store/hooks'
import LoadingSpinner from '../../../components/LoadingSpinner'
import { Form, FormItem, FormField, FormMessage, FormControl } from '@/components/ui/form'
import { PayloadAction } from '@reduxjs/toolkit'

export const loginUserSchema = z.object({
  email: z
    .string({ required_error: 'Email is required' })
    .email({ message: 'Please enter a valid email address.' }),
  password: z.string({ required_error: 'Password is required.' })
})

export default function LoginForm() {
  const [passwordVisible, setPasswordVisible] = useState<boolean>(false)
  const { loading, success, error } = useSelector(state => state.auth)
  const dispatch = useDispatch()
  const router = useRouter()

  const form = useForm<z.infer<typeof loginUserSchema>>({
    resolver: zodResolver(loginUserSchema)
  })

  useEffect(() => {
    if (error != undefined) {
      form.setError('email', {
        type: 'server',
        message: error
      })
    }
  }, [error])

  const onSubmit = async (values: z.infer<typeof loginUserSchema>) => {
    const { email, password } = values
    const response = await dispatch(loginUser({ email, password }))
    
    // check if the login was successful
    if (loginUser.fulfilled.match(response)) {
      router.push('/dashboard')
    }
  }

  return (
    <Form {...form}>
      <form autoComplete="off" onSubmit={form.handleSubmit(onSubmit)} className="flex flex-col gap-4">
        <FormField
          control={form.control}
          name="email"
          render={({ field }) => (
            <FormItem>
              <FormControl>
                <Input
                  className="h-12 focus-visible:border-2 focus-visible:border-blue-500 focus-visible:ring-0"
                  placeholder="Email"
                  {...field}
                  onChange={(e) => {
                    field.onChange(e)
                    form.clearErrors('email')
                  }}
                />
              </FormControl>
              <FormMessage className="text-xs" />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="password"
          render={({ field }) => (
            <FormItem>
              <FormControl>
                <div className="relative">
                  <Input
                    className="h-12 focus-visible:border-2 focus-visible:border-blue-500 focus-visible:ring-0"
                    placeholder="Password"
                    type={!passwordVisible ? 'password' : 'text'}
                    {...field}
                  />
                  <div className="cursor-pointer" onClick={() => setPasswordVisible(prev => !prev)}>
                    {!passwordVisible ? (
                      <Eye className="absolute right-3 top-3 text-gray-500" />
                    ) : (
                      <EyeOff className="absolute right-3 top-3 text-gray-500" />
                    )}
                  </div>
                </div>
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button type="submit" className="text-md h-12 bg-zinc-600 hover:bg-zinc-700">
          {loading ? <LoadingSpinner /> : 'Login'}
        </Button>
      </form>
    </Form>
  )
}

