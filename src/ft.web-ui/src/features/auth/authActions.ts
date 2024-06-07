import Cookie from 'js-cookie'
import axios, { AxiosError, AxiosResponse } from 'axios'
import { createAsyncThunk } from '@reduxjs/toolkit'

export type FetchError = {
  data: { message: string; errors: string[] | null }
}

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
  { rejectValue: FetchError }
>('auth/loginUser', async ({ email, password }, thunkAPI) => {
  try {
    const response: AxiosResponse<LoginResponse> = await axios.post(
      'http://localhost:5000/api/auth/login',
      {
        email,
        password
      }
    )
    const { accessToken, refreshToken } = response.data

    Cookie.set('accessToken', accessToken)
    Cookie.set('refreshToken', refreshToken)

    return { message: 'Login Success' }
  } catch (e) {
    if (axios.isAxiosError(e) === true) {
      if (!e.response) {
        // this conditional is to handle if there is no network,
        // a network error eventhough is an axios error, must be
        // handled gracefully
        return thunkAPI.rejectWithValue({
          data: {
            message: "An unexpected error has occured please try again",
            errors: null
          }
        })
      }

      return thunkAPI.rejectWithValue({
        data: e.response?.data
      })
    }

    return thunkAPI.rejectWithValue({
      data: {
        message: 'An unexpected error has occured please try again.',
        errors: null
      }
    })
  }
})
