import './globals.css'
import '@mantine/core/styles.css'
import '@mantine/dates/styles.css'
import type { Metadata } from 'next'
import ReduxProvider from './components/ReduxProvider'

import { ColorSchemeScript, MantineProvider } from '@mantine/core'

import { cn } from '@/lib/utils'

export const metadata: Metadata = {
  title: 'Financial Tracker',
  description: 'Track your finances with ease.'
}

export default function RootLayout({
  children
}: Readonly<{
  children: React.ReactNode
}>) {
  return (
    <html lang="en">
      <head>
        <ColorSchemeScript />
      </head>
      <body
        className={cn(
          { 'debug-screens': process.env.NODE_ENV === 'development' },
          'min-h-screen w-full bg-white font-poppins text-black'
        )}
      >
        <ReduxProvider>
          <MantineProvider>{children}</MantineProvider>
        </ReduxProvider>
      </body>
    </html>
  )
}
