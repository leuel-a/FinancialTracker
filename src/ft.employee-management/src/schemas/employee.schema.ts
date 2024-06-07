import { z } from 'zod'

const emergencyEmailPhoneSchema = z
  .object({
    emergencyEmail: z.string().email({ message: 'Must be a valid email' }).optional(),
    emergencyPhone: z.string().optional()
  })
  .refine(data => data.emergencyEmail || data.emergencyPhone, {
    message: 'Either email or phone is required for the emergency contact.'
  })

export const emergencyContactSchema = z
  .object({
    emergencyFirstName: z.string({ required_error: 'First name is required' }),
    emergencyMiddleName: z.string().optional(),
    emergencyLastName: z.string({ required_error: 'Last name is required' }),
    relationship: z.string({ required_error: 'Please specify the relationship to the employee' })
  })
  .and(emergencyEmailPhoneSchema)

export const employeeSalarySchema = z.object({
  baseSalary: z.number({ required_error: 'Base salary is required.' }),
  effectiveDate: z
    .string({ required_error: 'Effective date is required.' })
    .refine(value => !isNaN(Date.parse(value)), {
      message: 'Effective date must be a valid date string.'
    })
})

const payload = {
  body: z
    .object({
      firstName: z.string({ required_error: 'First name is required' }),
      lastName: z.string({ required_error: 'Last name is required' }),
      middleName: z.string().optional(),
      preferedName: z.string().optional(),
      dateOfBirth: z
        .string({ required_error: 'Date of birth is required.' })
        .refine(value => !isNaN(Date.parse(value)), {
          message: 'Date of birth must be a valid date string.'
        }),
      gender: z.string({ required_error: 'Gender of is required' }),
      personalEmail: z.string().email({ message: 'Must be a valid email' }).optional(),
      workEmail: z
        .string({ required_error: 'Work email is required' })
        .email({ message: 'Must be a valid email' }),
      city: z.string().optional(),
      country: z.string().optional(),
      subCity: z.string().optional(),
      phoneNumber: z.string().optional(),
    })
    .and(emergencyContactSchema)
    .and(employeeSalarySchema)
}

const params = {
  params: z.object({
    employeeId: z.string({
      required_error: 'Employee ID is required.'
    })
  })
}

export const createEmployeeSchema = z.object({
  ...payload
})

export const updateEmployeeSchema = z.object({
  ...payload,
  ...params
})

export const deleteEmployeeSchema = z.object({
  ...params
})

export const getEmployeeSchema = z.object({
  ...params
})

export type CreateEmployeeInput = z.infer<typeof createEmployeeSchema>
export type UpdateEmployeeInput = z.infer<typeof updateEmployeeSchema>
export type DeleteEmployeeInput = z.infer<typeof deleteEmployeeSchema>
export type GetEmployeeInput = z.infer<typeof getEmployeeSchema>
