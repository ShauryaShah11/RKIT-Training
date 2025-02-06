using System.Linq;
using System;
using System.Reflection;

namespace AdvanceC_FinalDemo.Extensions
{
    public static class ModelExtensions
    {
        /// <summary>
        /// Converts a Data Transfer Object (DTO) to a Plain Old CLR Object (POCO) using reflection.
        /// It maps the properties of the DTO to the matching properties in the POCO by name and type.
        /// </summary>
        /// <typeparam name="TPoco">The type of the POCO to which the DTO will be converted.</typeparam>
        /// <param name="dto">The DTO object to be converted to a POCO.</param>
        /// <returns>A new instance of the POCO with the properties mapped from the DTO.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the DTO is null.</exception>
        public static TPoco ToPoco<TPoco>(this object dto) where TPoco : new()
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "DTO cannot be null");

            // Create a new instance of the POCO
            TPoco poco = new TPoco();

            // Get the properties of the DTO and POCO
            PropertyInfo[] dtoProperties = dto.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] pocoProperties = typeof(TPoco).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var dtoProp in dtoProperties)
            {
                // Find matching properties by name
                PropertyInfo pocoProp = pocoProperties.FirstOrDefault(p => p.Name == dtoProp.Name && p.PropertyType == dtoProp.PropertyType);

                if (pocoProp != null)
                {
                    try
                    {
                        // Set the value of the matching property in the POCO
                        pocoProp.SetValue(poco, dtoProp.GetValue(dto));
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions that occur during the property assignment
                        Console.WriteLine($"Error mapping property {dtoProp.Name}: {ex.Message}");
                    }
                }
            }

            return poco;
        }

        /// <summary>
        /// Converts a Plain Old CLR Object (POCO) to a Data Transfer Object (DTO) using reflection.
        /// It maps the properties of the POCO to the matching properties in the DTO by name and type.
        /// </summary>
        /// <typeparam name="TDto">The type of the DTO to which the POCO will be converted.</typeparam>
        /// <param name="poco">The POCO object to be converted to a DTO.</param>
        /// <returns>A new instance of the DTO with the properties mapped from the POCO.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the POCO is null.</exception>
        public static TDto ToDto<TDto>(this object poco) where TDto : new()
        {
            if (poco == null)
                throw new ArgumentNullException(nameof(poco), "POCO cannot be null");

            // Create a new instance of the DTO
            TDto dto = new TDto();

            // Get the properties of the POCO and DTO
            PropertyInfo[] pocoProperties = poco.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] dtoProperties = typeof(TDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var pocoProp in pocoProperties)
            {
                // Find matching properties by name
                PropertyInfo dtoProp = dtoProperties.FirstOrDefault(p => p.Name == pocoProp.Name && p.PropertyType == pocoProp.PropertyType);

                if (dtoProp != null)
                {
                    try
                    {
                        // Set the value of the matching property in the DTO
                        dtoProp.SetValue(dto, pocoProp.GetValue(poco));
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions that occur during the property assignment
                        Console.WriteLine($"Error mapping property {pocoProp.Name}: {ex.Message}");
                    }
                }
            }

            return dto;
        }
    }
}