import React from 'react'
import SettingsSideNavBar from './components/SettingSideNavBar'
import PageTitle from '../../components/PageTitle'

interface SettingsLayoutProps {
  children: React.ReactNode
}

export default function SettingsLayout({ children }: SettingsLayoutProps) {
  return (
    <div>
      <PageTitle title="Settings" />
      <div className='flex gap-5  bg-red-50'>
        <SettingsSideNavBar />
        {children}
      </div>
    </div>
  )
}
