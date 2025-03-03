import { useQuery } from '@tanstack/react-query';

const SalesGoalsQuarterlyNeeded = () => {
    //const { data, error, isLoading } = useQuery({ queryKey: ['users'], queryFn: fetchUsers })
    
    //if(isLoading) return <div>Loading...</div>
    //if(error) return <div>Error: {error.message}</div>
    
  return (
    <>
    <div style={{ display: 'inline-block' }}>
        <div style={{ display: 'inline-flex', marginBottom: '10px' }}>
            <input type="number" className="form-control" style={{ maxWidth: '150px' }} />
            <label style={{ alignContent: 'center', marginLeft: '5px' }}> Average Sales Price </label>
            
        </div>

        <div style={{ display: 'inline-flex', marginBottom: '10px' }}>
            <input type="number" className="form-control" style={{ maxWidth: '150px' }} />
            <label style={{ alignContent: 'center', marginLeft: '5px' }}> Commission Rate </label>
        </div>
    </div>
    </>
  );
};

export default SalesGoalsQuarterlyNeeded;