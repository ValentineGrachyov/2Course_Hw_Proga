﻿using CustomProtocol;

/// <summary>
/// Проверка ПАКЕТОВ
/// </summary>
//var packet = XPacket.Create(1, 0);

//packet.SetValue(0, 123);
//packet.SetValue(1, 123D);
//packet.SetValue(2, 123F);
//packet.SetValue(3, false);

//var packetBytes = packet.ToPacket();
//var parsedPacket = XPacket.Parse(packetBytes);

//Console.WriteLine($"int: {parsedPacket.GetValue<int>(0)}\n" +
//                  $"double: {parsedPacket.GetValue<double>(1)}\n" +
//                  $"float: {parsedPacket.GetValue<float>(2)}\n" +
//                  $"bool: {parsedPacket.GetValue<bool>(3)}");


/// <summary>
/// Проверка сериализатора
///</summary>

var t = new TestPacket
{
    TestNumber = 12345,
    TestDouble = 123.45D,
    TestBoolean = true
};

var packet = XFieldAttribute.Serialize(0, 0, t);
var tDes = XFieldAttribute.Deserialize<TestPacket>(packet);

if (tDes.TestBoolean)
{
    Console.WriteLine($"Number = {tDes.TestNumber}\n" +
                      $"Double = {tDes.TestDouble}");
}

