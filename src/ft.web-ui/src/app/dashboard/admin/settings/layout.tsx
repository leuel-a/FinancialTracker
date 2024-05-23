import React from 'react'
import SettingsSideNavBar from './components/SettingSideNavBar'
import PageTitle from '../../components/PageTitle'

interface SettingsLayoutProps {
  children: React.ReactNode
}

export default function SettingsLayout({ children }: SettingsLayoutProps) {
  return (
    <div className="flex h-full flex-col gap-6">
      <PageTitle title="Settings" />
      <div className="grid h-full grid-cols-12">
        <div className='col-span-2 border-r-[1px]'>
          <SettingsSideNavBar className=" rounded-lg" />
        </div>
        <div className="col-span-10">{children}</div>
      </div>
    </div>
  )
}
