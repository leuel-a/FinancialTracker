import { configureStore } from '@reduxjs/toolkit'
import { roleApi } from '@/features/role/roleSlice'
import { userApi } from '@/features/user/userSlice'
import authReducer from '@/features/auth/authSlice'
import { employeesApi } from '@/features/employees/employeesApi'
import { categoriesApi } from '@/features/categories/categoriesApi'
import { transactionApi } from '@/features/transaction/transactionSlice'

export const store = configureStore({
  reducer: {
    auth: authReducer,
    [roleApi.reducerPath]: roleApi.reducer,
    [userApi.reducerPath]: userApi.reducer,
    [transactionApi.reducerPath]: transactionApi.reducer,
    [categoriesApi.reducerPath]: categoriesApi.reducer,
    [employeesApi.reducerPath]: employeesApi.reducer
  },
  middleware: getDefaultMiddleware =>
    getDefaultMiddleware()
      .concat(roleApi.middleware)
      .concat(userApi.middleware)
      .concat(transactionApi.middleware)
      .concat(categoriesApi.middleware)
      .concat(employeesApi.middleware)
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
