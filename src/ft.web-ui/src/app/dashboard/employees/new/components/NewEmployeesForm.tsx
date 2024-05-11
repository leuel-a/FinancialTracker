'use client'

import { useMultiStepForm } from '@/hooks/useMultiStepForm'
import { Button } from '@/components/ui/button'
import PersonalInformation from './PersonalInformation'
import EmergencyContactInformation from './EmergencyContactInformation'

export default function NewEmployeeForm() {
  const { currentStepIndex, currentStep, steps, onNext, onPrevious } = useMultiStepForm([
    ({
      isFirstStep: isFirstStep,
      isLastStep: isLastStep,
      onPrevious: onPrevious,
      onNext: onNext
    }) => (
      <PersonalInformation
        isFirstStep={isFirstStep}
        isLastStep={isLastStep}
        onNext={onNext}
        onPrevious={onPrevious}
      />
    ),
    ({
      isFirstStep: isFirstStep,
      isLastStep: isLastStep,
      onPrevious: onPrevious,
      onNext: onNext
    }) => (
      <EmergencyContactInformation
        isFirstStep={isFirstStep}
        isLastStep={isLastStep}
        onNext={onNext}
        onPrevious={onPrevious}
      />
    )
    // props => <EmergencyContactInformation {...props} />
  ])

  const stepIndicators = Array.from({ length: steps.length }, (_, i) => (
    <div className={`h-2 w-10 rounded ${i == currentStepIndex ? 'bg-black' : 'bg-gray-300'}`}></div>
  ))
  return (
    <div className="mt-5 flex flex-col gap-5 rounded-md border border-gray-200 py-8 shadow-md">
      <div className="flex items-center justify-between px-8">
        <p className="text-sm font-semibold">
          Step {currentStepIndex + 1} of {steps.length}
        </p>
        <div className="flex gap-2">{stepIndicators}</div>
      </div>
      {currentStep}
    </div>
  )
}
