using System.Data.SqlClient;

namespace tutorial5.Services;

public class AnimalsService
{
    private readonly string _connectionString;

    public AnimalsService(string connectionString)
    {
        _connectionString = connectionString;
    }
    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        var animals = new List<Animal>();
        using var con = new SqlConnection(_connectionString);
        con.Open();
        
        string orderByClause = "ORDER BY ";
        switch (orderBy.ToLower())
        {
            case "description":
                orderByClause += "Description ASC";
                break;
            case "category":
                orderByClause += "Category ASC";
                break;
            case "area":
                orderByClause += "Area ASC";
                break;
            default:
                orderByClause += "Name ASC";
                break;
        }

        using var cmd = new SqlCommand($"SELECT * FROM Animal {orderByClause}", con);
        using var dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            var animal = new Animal
            {
                idAnimal = (int)dr["IdAnimal"],
                Name = dr["Name"].ToString(),
                Description = dr["Description"].ToString(),
                Category = dr["Category"].ToString(),
                Area = dr["Area"].ToString()
            };
            animals.Add(animal);
        }

        return animals;
    }

    public int AddAnimal(Animal animal)
    {
        using var con = new SqlConnection(_connectionString);
        con.Open();
        using var cmd = new SqlCommand(@"INSERT INTO Animal (idAnimal, Name, Description, Category, Area)VALUES (@idAnimal, @Name, @Description, @Category, @Area)", con);
        cmd.Parameters.AddWithValue("@idAnimal", animal.idAnimal);
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);
        
        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }
    
    public int UpdateAnimal(int id, Animal animal)
    {
        using var con = new SqlConnection(_connectionString);
        con.Open();
        
        using var cmd = new SqlCommand("UPDATE Student SET Name=@Name, Description=@Description, Category=@Category, Area=@Area WHERE idAnimal = @idAnimal", con);
        cmd.Parameters.AddWithValue("@idAnimal", id);
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);
        
        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }
    
    public int DeleteAnimal(int id)
    {
        using var con = new SqlConnection(_connectionString);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "DELETE FROM Student WHERE idAnimal = @idAnimal";
        cmd.Parameters.AddWithValue("@idAnimal", id);
        
        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }
}
