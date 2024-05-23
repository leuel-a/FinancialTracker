import { createApi } from '@reduxjs/toolkit/query/react'
import { baseQueryWithAuth } from '@/lib/customBaseQuery'
import { Role } from '@/types/role'

const roleApi = createApi({
  reducerPath: 'roleApi',
  baseQuery: baseQueryWithAuth,
  endpoints: builder => ({
    getRoles: builder.query<Role[], void>({ query: () => '/api/roles' }),
    createRole: builder.mutation({
      query: (role: Partial<Role>) => ({ url: '/api/roles', method: 'POST', body: role })
    })
  })
})

export const { useGetRolesQuery, useCreateRoleMutation } = roleApi
export default roleApi
