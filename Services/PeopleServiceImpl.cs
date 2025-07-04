namespace Respuestas_HTTTP.Services {
    public class PeopleServiceImpl : IPeopleService {
        bool IPeopleService.Validate(People people) {
            if (string.IsNullOrEmpty(people.Name)  
                || people.Name.Length > 50)
                return false;

            return true;
        }
    }
}
