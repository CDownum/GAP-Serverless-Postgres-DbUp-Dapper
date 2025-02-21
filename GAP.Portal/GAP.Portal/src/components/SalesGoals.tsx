import { FC } from 'react';

interface SalesGoalsProps {
  id: string;
}

const SalesGoals: FC<SalesGoalsProps> = ({ id }) => {
  return (
    <>
     <div className='row position-relative'>
    <div className='col-12'>
        <div className='row' style={{ marginRight: '10px' }}>
            <div className='col-12'>
                 SALES GOALS: {id}
                {/* <GapQuarterlyGoalsSalesNeeded SalesGoal="@salesGoal"  /> */}
            </div>
        </div>
        <div className='row' style={{ marginRight: '10px' }}>
            <div className='col-8' style={{ float: 'right' }}>
                {/* <GapQuarterlyGoalsGrossSales SalesGoal="@salesGoal" SalesGoalQuarters="@salesGoal.SalesGoalQuarters" /> */}
            </div>
        </div>
        <div className='row' style={{ marginRight: '10px' }}>
            <div className='col-12'>
                <div className='row'>
                    {/* @foreach (var quarter in salesGoal.SalesGoalQuarters)
                    {
                        <div className='col-12 col-md-6 col-lg-3' style="display:inline-flex">
                            <GapQuarterlyGoalCard SalesGoalQuarter="@quarter" />
                        </div>
                    } */}
                </div>
            </div>
        </div>
    </div>
</div>
    </>
  );
};

export default SalesGoals;