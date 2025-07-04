namespace Respuestas_HTTTP.Services {
    public class People2ServiceImpl : IPeopleService {
        bool IPeopleService.Validate(People people) {
            if (string.IsNullOrEmpty(people.Name)  
                || people.Name.Length > 50 
                || people.Name.Length < 3)
                return false;

            return true;
        }
    }
}
