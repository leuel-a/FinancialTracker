export type Employee = {
  id: number 
  firstName: string
  lastName: string
  dateOfBirth?: string | null
  gender: string
  email: string
  phoneNumber: string
  type: string
  hireDate: string
  bonus: number,
  salary: number
}
const testEmployee: Employee = {
  'id': 1002,
  'firstName': 'Nathnael',
  'lastName': 'Gebreselassie',
  'email': 'nathnael.gebreselassie@gmail.com',
  'dateOfBirth': null,
  'hireDate': '6/12/2023 12:00:00 AM',
  'gender': 'male',
  'phoneNumber': '0932152547',
  'bonus': 5000,
  'salary': 25000,
  'type': 'FullTime'
}