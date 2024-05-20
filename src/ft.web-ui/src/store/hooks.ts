import {
  useDispatch as useReactReduxDispatch,
  useSelector as useReactRedusSelector
} from 'react-redux'
import { RootState, AppDispatch } from './index'

export const useDispatch = useReactReduxDispatch.withTypes<AppDispatch>()
export const useSelector = useReactRedusSelector.withTypes<RootState>()
