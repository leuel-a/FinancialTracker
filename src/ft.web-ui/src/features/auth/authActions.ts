import Cookie from 'js-cookie'
import { createAsyncThunk } from '@reduxjs/toolkit'
import axios, { AxiosError, AxiosResponse } from 'axios'
import { ValidationError, ValidationSuccess } from '@/types/validation'

type LoginResponse = {
  accessToken: string
  refreshToken: string
}

// type LoginSuccess = LoginResponse & ValidationSuccess
type LoginRequest = {
  email: string
  password: string
}

export const loginUser = createAsyncThunk<
  { message: string },
  LoginRequest,
  { rejectValue: ValidationError }
>('auth/loginUser', async ({ email, password }, thunkAPI) => {
  try {
    const response: AxiosResponse<LoginResponse> = await axios.post('http://localhost:5000/api/auth/login', {
      email,
      password
    })
    const { accessToken, refreshToken } = response.data

    Cookie.set('accessToken', accessToken)
    Cookie.set('refreshToken', refreshToken)

    return { message: 'Login Success' }
  } catch (e) {
    const errorResponse = e as AxiosError<ValidationError>
    const { response } = errorResponse

    const error = response?.data as ValidationError
    return thunkAPI.rejectWithValue(error)
  }
})
