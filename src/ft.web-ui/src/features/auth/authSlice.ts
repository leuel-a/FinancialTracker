import { User } from '@/types/user'
import { loginUser } from './authActions'
import { createSlice } from '@reduxjs/toolkit'
import { ValidationError } from '@/types/validation'
import { stat } from 'fs'

interface AuthState {
  loading: boolean
  user?: User
  error?: {
    message: string,
    errors: null | string[]
  }
  success: boolean
}

const initialState: AuthState = {
  loading: false,
  success: false
}

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {},
  extraReducers: builder => {
    // Add case for a pending login of user
    builder.addCase(loginUser.pending, state => {
      state.loading = true
      state.error = undefined
    })

    // Add case for a successful login of user
    builder.addCase(loginUser.fulfilled, (state, action) => {
      state.success = true
      state.loading = false
      state.error = undefined
    })

    // Add case for a failed login of user
    builder.addCase(loginUser.rejected, (state, action) => {
      state.loading = false
      state.error = action.payload?.data
    })
  }
})

export default authSlice.reducer
