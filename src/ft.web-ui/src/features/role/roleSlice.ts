import { createApi } from '@reduxjs/toolkit/query/react'
import { baseQueryWithAuth } from '@/lib/customBaseQuery'
import { Role } from '@/types/role'

export const roleApi = createApi({
  reducerPath: 'roleApi',
  baseQuery: baseQueryWithAuth,
  endpoints: builder => ({
    getRoles: builder.query<Role[], void>({ query: () => '/api/roles' }),
    createRole: builder.mutation({
      query: (role: Partial<Role>) => ({ url: '/api/roles', method: 'POST', body: role })
    }),
    deleteRole: builder.mutation({
      query: id => ({ url: `/api/roles/${id}`, method: 'DELETE' })
    })
  })
})

export const { useGetRolesQuery, useCreateRoleMutation, useDeleteRoleMutation } = roleApi

