import SideBar from './components/SideBar'

export default function DashboardLayout({ children }: { children: React.ReactNode }) {
  return (
    <div className="flex justify-center">
      <SideBar />
      <main className="h-screen w-full bg-red-50">{children}</main>
    </div>
  )
}
