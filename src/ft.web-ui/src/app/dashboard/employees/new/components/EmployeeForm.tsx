'use client'

import { z } from 'zod'
import { useRouter } from 'next/navigation'
import * as React from 'react'
import { useForm } from 'react-hook-form'
import { Employee } from '@/types/employee'
import { Input } from '@/components/ui/input'
import { Button } from '@/components/ui/button'
import { zodResolver } from '@hookform/resolvers/zod'
import { useCreateEmployeeMutation } from '@/features/employees/employeesApi'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form'
import { useToast } from '@/components/ui/use-toast'

const employeeSchema = z.object({
  firstName: z.string({ required_error: 'First name is required.' }),
  lastName: z.string({ required_error: 'LastName is required.' }),
  email: z.string({ required_error: 'Email is required.' }).email({ message: 'Must be a valid email.' }),
  dateOfBirth: z.string().optional(),
  hireDate: z.string({ required_error: ' Hire Date is required.' }),
  gender: z.string({ required_error: 'Gender is required.' }),
  phoneNumber: z.string().optional(),
  bonus: z.string({ required_error: 'Bonus is required, 0 is also valid.' }),
  salary: z.string({ required_error: 'Salary for the employee is required.' }),
  employeeType: z.string({ required_error: 'Employee Type is required.' })
})

export default function EmployeeForm() {
  const form = useForm<z.infer<typeof employeeSchema>>({
    resolver: zodResolver(employeeSchema)
  })
  const router = useRouter()
  const { toast } = useToast()
  const [createEmployee, result] = useCreateEmployeeMutation()

  const formatDate = (date: string) => {
    const [year, month, day] = date.split('-')
    return `${year}/${month}/${day}`
  }

  async function onSubmit(values: z.infer<typeof employeeSchema>) {
    const { bonus, salary, employeeType, dateOfBirth, hireDate, ...rest } = values

    const dob = formatDate(dateOfBirth as string)
    const hire = formatDate(hireDate as string)

    const employee: Partial<Employee> = {
      ...rest,
      dateOfBirth: dob,
      hireDate: hire,
      type: employeeType,
      bonus: parseFloat(bonus),
      salary: parseFloat(salary)
    }

    const result = await createEmployee(employee)
    if ('error' in result) {
      console.error(result.error)
    } else {
      const { data } = result
      toast({
        title: 'Employee created successfully.',
        description: `Welcome ${data.firstName} ${data.lastName}!`,
      })
      return router.push('/dashboard/employees')
    }
  }

  return (
    <div>
      <div className="mt-4 space-y-6">
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
            <div className="grid grid-cols-2 gap-4">
              <FormField
                control={form.control}
                name="firstName"
                render={({ field }) => {
                  return (
                    <FormItem>
                      <FormLabel className="font-semibold">First Name</FormLabel>
                      <FormControl>
                        <Input placeholder="First Name..." {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )
                }}
              />
              <FormField
                control={form.control}
                name="lastName"
                render={({ field }) => {
                  return (
                    <FormItem>
                      <FormLabel className="font-semibold">Last Name</FormLabel>
                      <FormControl>
                        <Input placeholder="Last Name..." {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )
                }}
              />
            </div>
            <div className="grid grid-cols-2 gap-4">
              <FormField
                control={form.control}
                name="email"
                render={({ field }) => {
                  return (
                    <FormItem>
                      <FormLabel className="font-semibold">Email</FormLabel>
                      <FormControl>
                        <Input type="email" placeholder="Email..." {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )
                }}
              />
              <FormField
                control={form.control}
                name="dateOfBirth"
                render={({ field }) => {
                  return (
                    <FormItem>
                      <FormLabel className="font-semibold">Date of Birth</FormLabel>
                      <FormControl>
                        <Input type="date" placeholder="Enter date of birth" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )
                }}
              />
            </div>
            <div className="grid grid-cols-2 gap-4">
              <FormField
                control={form.control}
                name="hireDate"
                render={({ field }) => {
                  return (
                    <FormItem>
                      <FormLabel className="font-semibold">Hire Date</FormLabel>
                      <FormControl>
                        <Input type="date" placeholder="Enter hire date" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )
                }}
              />
              <FormField
                control={form.control}
                name="gender"
                render={({ field }) => {
                  return (
                    <FormItem>
                      <FormLabel className="font-semibold">Gender</FormLabel>
                      <Select onValueChange={field.onChange} defaultValue={field.value}>
                        <FormControl>
                          <SelectTrigger>
                            <SelectValue placeholder="Please select your gender..." className="text-gray-500" />
                          </SelectTrigger>
                        </FormControl>
                        <SelectContent>
                          <SelectItem value="male">Male</SelectItem>
                          <SelectItem value="female">Female</SelectItem>
                        </SelectContent>
                      </Select>
                      <FormMessage />
                    </FormItem>
                  )
                }}
              />
            </div>
            <div className="grid grid-cols-2 gap-4">
              <FormField
                control={form.control}
                name="salary"
                render={({ field }) => {
                  return <FormItem>
                    <FormLabel className="font-semibold">Salary</FormLabel>
                    <FormControl>
                      <Input type="number" placeholder="12,235.45" {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                }}
              />
              <FormField
                control={form.control}
                name="bonus"
                render={({ field }) => {
                  return <FormItem>
                    <FormLabel className="font-semibold">Bonus</FormLabel>
                    <FormControl>
                      <Input type="number" placeholder="1.25" {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                }}
              />
            </div>
            <div className="grid grid-cols-2 gap-4">
              <FormField
                control={form.control}
                name="phoneNumber"
                render={({ field }) => {
                  return <FormItem>
                    <FormLabel className="font-semibold">Phone Number</FormLabel>
                    <FormControl>
                      <Input type="tel" placeholder="Phone Number..." {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                }}
              />
              <FormField
                control={form.control}
                name="employeeType"
                render={({ field }) => {
                  return <FormItem>
                    <FormLabel className="font-semibold">Employee Type</FormLabel>
                    <Select onValueChange={field.onChange} defaultValue={field.value}>
                      <FormControl>
                        <SelectTrigger>
                          <SelectValue placeholder="Please select employee type..." className="text-gray-500" />
                        </SelectTrigger>
                      </FormControl>
                      <SelectContent>
                        <SelectItem value="FullTime">Full Time</SelectItem>
                        <SelectItem value="PartTime">Part Time</SelectItem>
                        <SelectItem value="Contract">Contract</SelectItem>
                        <SelectItem value="Intern">Intern</SelectItem>
                      </SelectContent>
                    </Select>
                  </FormItem>
                }}
              />
            </div>
            <Button type="submit" className="w-full bg-zinc-800">Submit</Button>
          </form>
        </Form>
      </div>
    </div>
  )
}
