'use client'

import React from 'react'

import { z } from 'zod'
import { format } from 'date-fns'
import { useForm } from 'react-hook-form'
import { CalendarIcon } from '@radix-ui/react-icons'
import { zodResolver } from '@hookform/resolvers/zod'
import { DatePicker } from '@mantine/dates'

import { Button } from '@/components/ui/button'
import useFormPersist from 'react-hook-form-persist'

import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage
} from '@/components/ui/form'

import { Popover, PopoverContent, PopoverTrigger } from '@/components/ui/popover'

import { Input } from '@/components/ui/input'

import {
  Select,
  SelectGroup,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
  SelectLabel
} from '@/components/ui/select'

import { cn } from '@/lib/utils'
import { cities } from '../data'
import { useMultiStepForm } from '@/hooks/useMultiStepForm'

type PersonalInformationProps = {
  isFirstStep: boolean
  isLastStep: boolean
  onPrevious: () => void
  onNext: () => void
}

const personalInformationSchema = z.object({
  firstName: z.string({ required_error: 'First name is required to continue' }),
  middleName: z.string().optional(),
  lastName: z.string({ required_error: 'Last name is required to continue' }),
  preferedName: z.string().optional(),
  gender: z.string({ required_error: 'Gender is required to continue' }),
  phoneNumber: z.string({ required_error: 'Phone number is required' }),
  dateofbirth: z.string({ required_error: 'Date of birth is required' }),
  personalEmail: z.string().email({ message: 'Invalid email address' }).optional(),
  workEmail: z
    .string({ required_error: 'Employees work email is required to continue' })
    .email({ message: 'Invalid email address' }),
  city: z.string().optional(),
  subcity: z.string().optional()
})

export default function PersonalInformation({
  isFirstStep,
  isLastStep,
  onPrevious,
  onNext
}: PersonalInformationProps) {
  const form = useForm<z.infer<typeof personalInformationSchema>>({
    resolver: zodResolver(personalInformationSchema)
  })

  const { watch, setValue } = form
  useFormPersist('personal-information', { watch, setValue })

  function onSubmit(values: z.infer<typeof personalInformationSchema>) {}

  return (
    <>
      <div className="bg-zinc-200 px-8 py-5 shadow-sm">
        <h2 className="text-xl font-semibold">Personal Information</h2>
        <p className="mt-1 text-sm font-light">Please enter the employees personal information</p>
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
                    <Input placeholder="Leuel..." {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="middleName"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Middle Name</FormLabel>
                  <FormControl>
                    <Input placeholder="A..." {...field} />
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
              name="preferedName"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Prefered Name</FormLabel>
                  <FormControl>
                    <Input placeholder="Leula..." {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="gender"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Gender</FormLabel>
                  <Select onValueChange={field.onChange} defaultValue={field.value}>
                    <FormControl>
                      <SelectTrigger className="text-gray-500">
                        <SelectValue placeholder="Please select the employees gender" />
                      </SelectTrigger>
                    </FormControl>
                    <SelectContent>
                      <SelectItem value="male">Male</SelectItem>
                      <SelectItem value="femail">Female</SelectItem>
                    </SelectContent>
                  </Select>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="dateofbirth"
              render={({ field }) => (
                <FormItem className="flex h-fit flex-col justify-between gap-2">
                  <FormLabel>Date of Birth</FormLabel>
                  <Popover>
                    <PopoverTrigger asChild>
                      <FormControl>
                        <Button
                          variant={'outline'}
                          className={cn(
                            'w-full pl-3 text-left font-normal',
                            !field.value && 'text-muted-foreground'
                          )}
                        >
                          {field.value ? format(field.value, 'PPP') : <span>Pick a date</span>}
                          <CalendarIcon className="ml-auto h-4 w-4" />
                        </Button>
                      </FormControl>
                    </PopoverTrigger>
                    <PopoverContent>
                      <DatePicker value={new Date(field.value)} onChange={field.onChange} />
                    </PopoverContent>
                  </Popover>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="personalEmail"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Personal Email</FormLabel>
                  <FormControl>
                    <Input placeholder="leuel.asfaw@gmail.com" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="workEmail"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Work Email</FormLabel>
                  <FormControl>
                    <Input placeholder="leuel.asfaw@info.tech" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="city"
              render={({ field }) => (
                <FormItem className="flex h-fit flex-col justify-between gap-2">
                  <FormLabel>City</FormLabel>
                  <Select onValueChange={field.onChange} defaultValue={field.value}>
                    <FormControl className={!field.value ? 'text-gray-500' : 'text-black'}>
                      <SelectTrigger className="w-full">
                        <SelectValue placeholder="Select City" />
                      </SelectTrigger>
                    </FormControl>
                    <SelectContent>
                      {cities.map(city => (
                        <SelectItem value={city.name.toLowerCase()}>{city.name.trim()}</SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="subcity"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Sub city</FormLabel>

                  <Select onValueChange={field.onChange} defaultValue={field.value}>
                    <FormControl className={!field.value ? 'text-gray-500' : 'text-black'}>
                      <SelectTrigger>
                        <SelectValue placeholder="Select Sub City" />
                      </SelectTrigger>
                    </FormControl>
                    <SelectContent>
                      {cities.map(city => (
                        <SelectGroup>
                          <SelectLabel>{city.name}</SelectLabel>
                          {city.subCities.map(subCity => (
                            <SelectItem value={subCity.toLowerCase()}>{subCity}</SelectItem>
                          ))}
                        </SelectGroup>
                      ))}
                    </SelectContent>
                  </Select>
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
                    <Input placeholder="+251929185488..." {...field} />
                  </FormControl>
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
                form.trigger().then(value => {
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
