﻿using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

public class AutoMoqDataAttribute : AutoDataAttribute {
    public AutoMoqDataAttribute() : base(() => {
        var fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
        return fixture;
    }) {
    }
}