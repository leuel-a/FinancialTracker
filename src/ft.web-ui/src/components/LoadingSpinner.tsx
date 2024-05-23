import React from 'react'
import { cn } from '@/lib/utils'

export default function LoadingSpinner({className, ...props}: React.HTMLAttributes<HTMLDivElement>) {
  return <div {...props} className={cn('h-6 w-6 animate-spin rounded-full border-b-2 border-current', className)} />
}
