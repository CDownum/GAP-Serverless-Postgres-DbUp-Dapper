import { User } from '../interfaces/User';
import gapApiClient from './gapApiClient';
import { Guid } from 'guid-typescript';

class UserDataService {

  getUserById(companyId: number, id: Guid) {
    return gapApiClient.get<User>(`/api/companies/${companyId}/users/${id}`);
  }

}
  
  export default new UserDataService();