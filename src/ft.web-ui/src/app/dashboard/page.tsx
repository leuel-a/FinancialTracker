export default function DashboardPage() {
  return (
    <div className="mt-3 px-5 text-white">
      <h1 className="text-3xl font-semibold text-gray-700">Welcome Back, John</h1>
      <div className="mt-5 grid grid-cols-3 gap-2">
        <div className="h-40 bg-gray-300">
          Total Revenue, show for a month and an indicator for increase in from last month
        </div>
        <div className="h-40 bg-gray-300"></div>
        <div className="h-40 bg-gray-300"></div>
      </div>
      <div className="mt-5 grid grid-cols-12 gap-2">
        <div className="col-span-8 h-96 bg-gray-300">
          Line graph that will be used to show the transactions overtime
        </div>
        <div className="col-span-4 h-96 bg-gray-300">Pie Chart for categorical analysis</div>
      </div>
    </div>
  )
}
