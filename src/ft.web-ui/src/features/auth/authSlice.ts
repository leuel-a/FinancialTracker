import { User } from '@/types/user'
import { loginUser } from './authActions'
import { createSlice } from '@reduxjs/toolkit'

interface AuthState {
  loading: boolean
  user?: User
  accessToken?: string
  refreshToken?: string
  error?: string
  errorMessages?: string[]
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
      state.errorMessages = undefined
    })
    
    // Add case for a successful login of user
    builder.addCase(loginUser.fulfilled, (state, action) => {
      state.success = true
      state.loading = false
      state.accessToken = action.payload.accessToken
      state.refreshToken = action.payload.refreshToken
      state.error = undefined
      state.errorMessages = undefined
    })

    // Add case for a failed login of user
    builder.addCase(loginUser.rejected, (state, action) => {
      state.loading = false
      state.error = action.payload?.message
      state.errorMessages = action.payload?.errors
    })
  }
})

export default authSlice.reducer
