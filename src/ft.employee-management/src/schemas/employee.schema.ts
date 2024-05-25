import { z } from 'zod'

const emailPhoneSchema = z.object({
  email: z.string().email({ message: 'Must be a valid email' }).optional(),
  phone: z.string().optional()
}).refine(data => data.email || data.phone, {
  message: 'Either email or phone is required.'
})

export const emergencyContactSchema = z.object({
  firstName: z.string({ required_error: 'First name is required' }),
  middleName: z.string().optional(),
  lastName: z.string({ required_error: 'Last name is required' }),
  relationship: z.string({ required_error: 'Please specify the relationship to the employee' })
})

export const employeeSalarySchema = z.object({
  baseSalary: z.number({ required_error: 'Base salary is required.' }),
  effectiveDate: z.date({ required_error: 'Effective date is required.' })
})

export const createEmployeeSchema = z.object({
  body: z.object({
    firstName: z.string({ required_error: 'First name is required' }),
    lastName: z.string({ required_error: 'Last name is required' }),
    middleName: z.string().optional(),
    preferredName: z.string().optional(),
    dateOfBirth: z.date({ required_error: 'Date of birth is required' }),
    gender: z.string({ required_error: 'Gender of is required' }),
    personalEmail: z.string().email({ message: 'Must be a valid email' }).optional(),
    workEmail: z.string({ required_error: 'Work email is required' }).email({ message: 'Must be a valid email' })
  }).and(emailPhoneSchema).and(emergencyContactSchema).and(employeeSalarySchema)
})

