import Cookies from 'js-cookie'
import { jwtDecode, JwtPayload } from 'jwt-decode'

interface ExtendedJwtPayload extends JwtPayload {
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'?: string
}

export const checkAdmin = (setIsAdmin: (isAdmin: boolean) => void) => {
  const accessToken = Cookies.get('accessToken')
  const decodedAccessToken = jwtDecode<ExtendedJwtPayload>(accessToken as string)

  const roleClaim = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
  if (decodedAccessToken[roleClaim] === 'Admin') {
    setIsAdmin(true)
  }
}