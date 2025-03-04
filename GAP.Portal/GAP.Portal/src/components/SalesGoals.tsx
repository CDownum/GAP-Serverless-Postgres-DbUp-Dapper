//import { useRecoilValue } from 'recoil';
//import { loggedInUserState } from '../recoil/atom/user';

const SalesGoals = () => {  
    //const {DisplayName} = useRecoilValue(loggedInUserState);

  return (
    <>
     <div className='row position-relative'>
    <div className='col-12'>
        <div className='row mr-10'>
            <div className='col-12'>
            <h1>Michael</h1>
            {/* <GapQuarterlyGoalsSalesNeeded SalesGoal="@salesGoal"  /> */}
            </div>
        </div>
        <div className='row mr-10'>
            <div className='col-8 float-right'>
                {/* <GapQuarterlyGoalsGrossSales SalesGoal="@salesGoal" SalesGoalQuarters="@salesGoal.SalesGoalQuarters" /> */}
            </div>
        </div>
        <div className='row mr-10'>
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