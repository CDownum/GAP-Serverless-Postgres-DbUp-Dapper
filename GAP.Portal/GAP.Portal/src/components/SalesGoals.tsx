import { useQuery } from '@tanstack/react-query';
import axios from 'axios';

interface User {
    id: number;
    name: string;
    email: string;
    phone: string;
    address: string;
 }

 const fetchUsers = async (): Promise<User[]> => { 
    const { data } = await axios.get('https://jsonplaceholder.typicode.com/users');
    return data;
  }

const SalesGoals = () => {
    const { data, error, isLoading } = useQuery({ queryKey: ['users'], queryFn: fetchUsers })
    
    if(isLoading) return <div>Loading...</div>
    if(error) return <div>Error: {error.message}</div>
    
  return (
    <>
     <div className='row position-relative'>
    <div className='col-12'>
        <div className='row' style={{ marginRight: '10px' }}>
            <div className='col-12'>
            {data ? (
                                data.map(user => (
                                    <div key={user.id}>{user.name}</div>
                                ))
                            ) : (
                                <div>No data available</div>
                            )}
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