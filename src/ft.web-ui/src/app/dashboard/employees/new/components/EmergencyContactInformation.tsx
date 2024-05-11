'use client'

import { z } from 'zod'
import { format } from 'date-fns'
import { useForm } from 'react-hook-form'
import useFormPersist from 'react-hook-form-persist'
import { zodResolver } from '@hookform/resolvers/zod'

import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import {
  Form,
  FormField,
  FormItem,
  FormLabel,
  FormControl,
  FormMessage
} from '@/components/ui/form'

type EmergencyContactInformationProps = {
  isFirstStep: any
  isLastStep: any
  onNext: any
  onPrevious: any
}

const emailPhoneSchema = z
  .object({
    email: z.string().email().optional(),
    phoneNumber: z.string().optional()
  })
  .refine(data => data.email || data.phoneNumber, {
    message: 'Email or phone number is required',
    path: ['email']
  })

const emergencyContactInformationSchema = z
  .object({
    firstName: z.string({ required_error: 'First name is required' }),
    lastName: z.string({ required_error: 'Last name is required' })
  })
  .and(emailPhoneSchema)

export default function EmergencyContactInformation({
  isFirstStep,
  isLastStep,
  onPrevious,
  onNext
}: EmergencyContactInformationProps) {
  const form = useForm<z.infer<typeof emergencyContactInformationSchema>>({
    resolver: zodResolver(emergencyContactInformationSchema)
  })

  const { trigger, watch, setValue } = form

  useFormPersist('emergency-contact-information', { watch, setValue })

  function onSubmit(values: z.infer<typeof emergencyContactInformationSchema>) {}

  return (
    <>
      <div className="bg-zinc-200 px-8 py-5 shadow-sm">
        <h2 className="text-xl font-semibold">Emergency Contact Information</h2>
        <p className="mt-1 text-sm font-light">
          Please enter the employees emergency contact information
        </p>
      </div>
      <Form {...form}>
        <form className="px-8" onSubmit={form.handleSubmit(onSubmit)}>
          <div className="mb-10 grid grid-cols-3 gap-x-5 gap-y-7">
            <FormField
              control={form.control}
              name="firstName"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>First Name</FormLabel>
                  <FormControl>
                    <Input placeholder="Nathnael" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="lastName"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Last Name</FormLabel>
                  <FormControl>
                    <Input placeholder="Gebreselassie..." {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="email"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Email</FormLabel>
                  <FormControl>
                    <Input placeholder="nathnael.gebreselassie@gmail.com" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="phoneNumber"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Phone Number</FormLabel>
                  <FormControl>
                    <Input placeholder="+251932152547" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
          </div>
          <div className="flex justify-end gap-4 px-8">
            {!isFirstStep && (
              <Button className="w-56" onClick={onPrevious}>
                Back
              </Button>
            )}
            <Button
              onClick={() => {
                trigger().then(value => {
                  if (value) onNext()
                })
              }}
              className="w-56"
              type="submit"
            >
              {isLastStep ? 'Submit' : 'Next'}
            </Button>
          </div>
        </form>
      </Form>
    </>
  )
}
