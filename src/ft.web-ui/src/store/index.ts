import { configureStore } from '@reduxjs/toolkit'
import { roleApi } from '@/features/role/roleSlice'
import { userApi } from '@/features/user/userSlice'
import authReducer from '@/features/auth/authSlice'
import { categoriesApi } from '@/features/categories/categoriesApi'
import { transactionApi } from '@/features/transaction/transactionSlice'

export const store = configureStore({
  reducer: {
    auth: authReducer,
    [roleApi.reducerPath]: roleApi.reducer,
    [userApi.reducerPath]: userApi.reducer,
    [transactionApi.reducerPath]: transactionApi.reducer,
    [categoriesApi.reducerPath]: categoriesApi.reducer
  },
  middleware: getDefaultMiddleware =>
    getDefaultMiddleware()
      .concat(roleApi.middleware)
      .concat(userApi.middleware)
      .concat(transactionApi.middleware)
      .concat(categoriesApi.middleware)
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
