import { configureStore } from '@reduxjs/toolkit'
import roleApi from '@/features/role/roleSlice'
import authReducer from '@/features/auth/authSlice'

export const store = configureStore({
  reducer: {
    auth: authReducer,
    [roleApi.reducerPath]: roleApi.reducer
  },
  middleware: getDefaultMiddleware => getDefaultMiddleware().concat(roleApi.middleware)
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
