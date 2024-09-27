namespace ProductsApi.Models;


/* Fields for the entity
- name : string
- price : decimal
- dateOfCreation: DateTime
- available: boolean
- quantity: int
*/
public class ProductItem
{
    public int Id { get; set;}
    public string Name { get; set;}

    public decimal Price { get; set;}

    public DateTime DateOfCreation { get; set;}

    public bool IsAvailable { get; set;}

    public int Quantity { get; set;}
}