import { baseQueryWithAuth } from '@/lib/customBaseQuery'
import { createApi } from '@reduxjs/toolkit/query/react'
import { User } from '@/types/user'

export const userApi = createApi({
  reducerPath: 'userApi',
  baseQuery: baseQueryWithAuth,
  endpoints: builder => ({
    getAllUsers: builder.query<User[], void>({
      query: () => `api/users`
    })
  })
})

export const { useGetAllUsersQuery } = userApi
