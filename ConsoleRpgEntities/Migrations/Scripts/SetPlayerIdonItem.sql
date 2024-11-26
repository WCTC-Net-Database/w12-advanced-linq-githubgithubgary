UPDATE Items SET PlayerId = null;

UPDATE Items SET PlayerId = 1 WHERE Id IN (10,5,6,26);

UPDATE Equipments SET WeaponId = 10, ArmorId = 5, AccessoryId = 26, PotionId = 6 WHERE Id = 1;
