import { Employee } from '@/types/employee'
import { PaginatedResponse } from '@/types/paginated'
import { createApi } from '@reduxjs/toolkit/query/react'
import { baseQueryWithAuth } from '@/lib/customBaseQuery'

export const employeesApi = createApi({
  reducerPath: 'employeesApi',
  baseQuery: baseQueryWithAuth,
  endpoints: builder => ({
    getAllEmployees: builder.query<PaginatedResponse<Employee>, string>({
      query: params => `api/employees?${params}`
    }),
    createEmployee: builder.mutation<Employee, Partial<Employee>>({
      query: (body) => ({
        url: 'api/employees',
        method: 'POST',
        body
      })
    })
  })
})

export const { useGetAllEmployeesQuery, useCreateEmployeeMutation } = employeesApi