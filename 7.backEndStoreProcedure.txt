USE [C27]
GO
/****** Object:  StoredProcedure [dbo].[AddressTest_Find]    Script Date: Fri  Mar  17 3:22:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[AddressTest_Find]
	@latpoint float(7),
	@lngpoint float(7),
	@radius float(2)
AS

BEGIN

SELECT 
		[Id],
		[Address1], [Address2], [City], [State], [ZipCode],
		[Latitude], [Longitude],
		distance_in_mi
FROM (
SELECT
		a.Id,
        a.Address1, a.Address2, a.City, a.State, a.ZipCode,
        [Latitude], [Longitude],
        p.distance_unit
                 * DEGREES(ACOS(COS(RADIANS(p.latpoint))
                 * COS(RADIANS([Latitude]))
                 * COS(RADIANS(p.longpoint) - RADIANS([Longitude]))
                 + SIN(RADIANS(p.latpoint))
                 * SIN(RADIANS([Latitude])))) AS distance_in_mi
FROM [dbo].[AddressesTest] AS a
JOIN (
        SELECT  @latpoint  AS latpoint,  @lngpoint AS longpoint,
                @radius AS radius,       69.0 AS distance_unit
    ) AS p ON 1=1
 WHERE [Latitude]
    BETWEEN p.latpoint  - (p.radius / p.distance_unit)
         AND p.latpoint  + (p.radius / p.distance_unit)
    AND [Longitude]
    BETWEEN p.longpoint - (p.radius / (p.distance_unit * COS(RADIANS(p.latpoint))))
    AND p.longpoint + (p.radius / (p.distance_unit * COS(RADIANS(p.latpoint))))
	/* add to skip the company itself  from the search */
) AS d
WHERE distance_in_mi <= @radius
ORDER BY distance_in_mi

END
  
/*------------TEST CODE ---------------------

DECLARE
	@latpoint float(7) = 33.6116239,
	@lngpoint float(7) = -117.8766065,
	@radius float(2) = 20

EXECUTE [dbo].[AddressTest_Find]
	@latpoint,
	@lngpoint,
	@radius

*/