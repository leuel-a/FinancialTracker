import axios, { AxiosError } from 'axios'
import { createAsyncThunk } from '@reduxjs/toolkit'
import { ValidationError, ValidationSuccess } from '@/types/validation'

interface LoginSuccess extends ValidationSuccess {
  accessToken: string
  refreshToken: string
}

interface LoginRequest {
  email: string
  password: string
}

export const loginUser = createAsyncThunk<
  LoginSuccess,
  LoginRequest,
  { rejectValue: ValidationError }
>('auth/loginUser', async ({ email, password }, thunkAPI) => {
  try {
    const response = await axios.post('http://localhost:5000/api/auth/login', { email, password })
    return response.data
  } catch (e) {
    const errorResponse = e as AxiosError<ValidationError>
    const { response } = errorResponse

    const error = response?.data as ValidationError
    return thunkAPI.rejectWithValue(error)
  }
})
